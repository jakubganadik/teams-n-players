import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import routes from 'virtual:generated-pages'
import App from './App.vue'

import '@unocss/reset/tailwind.css'
import './styles/main.css'
import 'uno.css'
import { VueQueryPlugin } from '@tanstack/vue-query'

const app = createApp(App)

app.use(createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
}))

app.use(createPinia())
app.use(VueQueryPlugin)

app.mount('#app')


