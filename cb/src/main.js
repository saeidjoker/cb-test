import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import '@babel/polyfill'
import {store} from "./store/state";

Vue.config.productionTip = false

new Vue({
  vuetify,
  store,
  render: h => h(App)
}).$mount('#app')
