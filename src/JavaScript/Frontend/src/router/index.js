import { createRouter, createWebHistory } from 'vue-router'
import DiscoverView from '../views/DiscoverView.vue'
import ForkDiscoverView from '../views/ForkDiscoverView.vue'
import StarDiscoverView from '../views/StarDiscoverView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      redirect: '/starred'
    },
    {
      path: '/discover',
      name: 'discover',
      component: DiscoverView
    },
    {
      path: '/forked',
      name: 'forked-discovery',
      component: ForkDiscoverView
    },
    {
      path: '/starred',
      name: 'starred-discovery',
      component: StarDiscoverView
    }
  ]
})

export default router
