<template>
  <v-container>
    <v-row class="text-center">
      <v-col cols="12" v-show="$store.state.isLoggedIn === false">
        <v-card>
          <v-card-title>Sign In</v-card-title>
          <v-card-text>
            <v-form v-model="validLoginForm">
              <v-row>
                <v-col cols="12">
                  <v-text-field label="User name" v-model="username" :rules="[requiredField]"/>
                </v-col>
                <v-col cols="12">
                  <v-text-field label="Password" type="password" v-model="password" :rules="[requiredField]"/>
                </v-col>
              </v-row>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-btn class="ma-1" :disabled="disableLogin" @click="login" block>
              Submit
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
      <v-col cols="12" v-show="$store.state.isLoggedIn && canPlay">
        <v-card>
          <v-card-title>Let's Play</v-card-title>
          <v-card-text>
            <v-btn class="ma-1" @click="play" block>
              Test your chance
            </v-btn>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="12">
        <v-card>
          <v-card-title>Finished Matches (Page: {{ (pageIndex + 1) }})</v-card-title>
          <v-card-text>
            <div v-for="a in finishedMatches" :key="a.matchId">
              <div class="mt-1 pa-2" style="border: 1px solid black;">
                Winner: <b>{{ a.winnerUserName }}</b> - Score: <b>{{ a.winnerScore }}</b>
                - Finished At: <b>{{ new Date(a.expiresTimestamp) }}</b>
              </div>
            </div>
          </v-card-text>
          <v-card-actions>
            <v-btn class="ma-1" @click="nextPage">
              Next Page
            </v-btn>
            <v-btn class="ma-1" @click="prevPage">
              Previous Page
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import {get, post} from '../shared/api'

export default {
  name: 'HelloWorld',

  data: () => ({
    validLoginForm: false,
    username: '',
    password: '',
    pageIndex: -1,
    totalFinishedMatches: 0,
    finishedMatches: [],
    canPlay: false,
    nextMatch: {
      id: '',
      expiresTimestamp: 0
    }
  }),

  computed: {
    requiredField() {
      return v => !!v || 'Required'
    },
    disableLogin() {
      return !this.validLoginForm;
    }
  },

  async mounted() {
    await this.nextPage()
  },

  methods: {

    async play() {
      try {
        await post("api/Play/" + this.nextMatch.id)
        alert("Thanks for playing")
      } catch (e) {
        alert("You already played this match")
      } finally {
        this.canPlay = false
      }
    },

    async getNextMatch() {
      try {
        const res = await get("api/Match/next-match");
        this.nextMatch.id = res.id;
        this.nextMatch.expiresTimestamp = res.expiresTimestamp
        this.canPlay = true;
      } catch (e) {
        alert("There's no match for you right now :-(")
        this.canPlay = false;
      }
    },

    async nextPage() {
      const res = await get("api/Match/list-finished-matches/" + (this.pageIndex + 1))
      this.finishedMatches = res.items
      this.totalFinishedMatches = res.total
      this.pageIndex++
    },

    async prevPage() {
      if (this.pageIndex === 0) return;
      const res = await get("api/Match/list-finished-matches/" + (this.pageIndex - 1))
      this.finishedMatches = res.items
      this.totalFinishedMatches = res.total
      this.pageIndex--
    },

    async login() {
      try {
        await post("api/authentication/sign-in", {
          "UserName": this.username,
          "Password": this.password
        })
        this.$store.state.isLoggedIn = true;
        this.$store.state.username = this.username;
        this.$store.state.password = this.password;

        await this.getNextMatch()

      } catch (ex) {
        alert('Invalid credentials');
      }
    }
  }
}
</script>
