import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    username: null,
    password: null,
    isLoggedIn: false
  }
})

export {store}
