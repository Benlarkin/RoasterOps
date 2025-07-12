import { createRouter, createWebHistory } from 'vue-router'
import Inventory from '@/views/Inventory.vue'
import Orders from '@/views/Orders.vue'

const routes = [
    { path: '/', redirect: '/inventory' },
    { path: '/inventory', component: Inventory },
    { path: '/orders', component: Orders },
]

export const router = createRouter({
    history: createWebHistory(),
    routes,
})
