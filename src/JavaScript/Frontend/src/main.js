import './assets/styles/main.css'
import '../node_modules/nprogress/nprogress.css' 

import NProgress from 'nprogress';
import { createPinia } from 'pinia'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

NProgress.configure({ showSpinner: false });

const app = createApp(App)
const pinia = createPinia()

app.use(router)
app.use(pinia)

app.mount('#app')
