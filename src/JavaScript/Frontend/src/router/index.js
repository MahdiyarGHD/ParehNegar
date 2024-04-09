import { createRouter, createWebHistory } from 'vue-router'
import DiscoverView from '../views/DiscoverView.vue'
import ForkDiscoverView from '../views/ForkDiscoverView.vue'
import StarDiscoverView from '../views/StarDiscoverView.vue'
import NProgress from 'nprogress';

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
      showNProgress: true,
      name: 'discover',
      component: DiscoverView
    },
    {
      path: '/forked',
      showNProgress: true,
      name: 'forked-discovery',
      component: ForkDiscoverView
    },
    {
      path: '/starred',
      name: 'starred-discovery',
      showNProgress: false,
      component: StarDiscoverView
    }
  ]
})

router.beforeResolve((to, from, next) => {
  if (to.name) {
      NProgress.start()
  }
  
  next()
})

router.afterEach(() => {
  NProgress.done()
})
export default router
