import { h, resolveComponent } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'

import DefaultLayout from '@/layouts/DefaultLayout'

const routes = [
  {
    path: '/home',
    name: 'Home',
    component: DefaultLayout,
    children: [
      {
        path: '/infusions/:id',
        name: 'Infusions Operations',
        component: () =>
          import(/* webpackChunkName: "dashboard" */ '@/views/pages/InfusionsCrud.vue')
      },
      {
        path: '/infusions',
        name: 'Infusions',
        component: () => import('@/views/pages/Infusions.vue'),
      },
      {
        path: '/medicines/:id',
        name: 'Test1',
        component: () =>
          import(/* webpackChunkName: "dashboard" */ '@/views/pages/Crud.vue')

      },
      {
        path: '/medicines',
        name: 'Medicines',
        component: () => import('@/views/pages/Medicines.vue'),
      }
    ],
  },
  {
    path: '/login',
    name: 'OurLogin',
    component: () => import('@/views/pages/Login.vue')
  },
  {
    path: '/pages',
    redirect: '/pages/404',
    name: 'Pages',
    component: {
      render() {
        return h(resolveComponent('router-view'))
      },
    },
    children: [
      {
        path: 'login',
        name: 'Login',
        component: () => import('@/views/pages/Login'),
      },
    ],
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
  scrollBehavior() {
    // always scroll to top
    return { top: 0 }
  },
})

router.beforeEach(async (to, from) => {
  if(to.path == '/app/baby_dose_expert_v1.0.apk')
    return true;

  var token = sessionStorage.getItem('pharmacyToken');
  if ((token && token != 'null') || to.path == '/login') {
    if(to.path == '/') {
      router.push('/medicines');
      return false;
    }
    else
      return true;
  }
  else {
    router.push('/login');
    return false;
  }

  router.push(to);
  return true;

})

export default router
