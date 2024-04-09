import { createRouter, createWebHistory } from 'vue-router'
import DiscoverView from '../views/DiscoverView.vue'
import ForkDiscoverView from '../views/ForkDiscoverView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: DiscoverView
    },
    
    {
      path: '/forked',
      name: 'forked-discovery',
      component: ForkDiscoverView
    }
  ]
})

export default router
