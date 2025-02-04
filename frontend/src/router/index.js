import { createRouter, createWebHistory } from 'vue-router';
import store from '../store'; // Импортируйте ваше хранилище Vuex
import Login from '../views/LoginPage.vue'; // Импортируйте ваш компонент логина

import UserManagement from '../components/manager/UserManagement.vue';
import CustomerManagement from '../components/manager/CustomerManagement.vue';
import ProductManagement from '../components/manager/ProductManagement.vue';
import OrderManagement from '../components/manager/OrderManagement.vue';


import ProductList from '../components/customer/ProductList.vue';
import Cart from '../components/customer/Cart.vue';
import OrderList from '../components/customer/OrderList.vue';

const routes = [
    {
        path: '/',
        name: 'Login',
        component: Login,
    },
    {
        path: '/login',
        name: 'Login',
        component: Login,
    },
    {
        path: '/manager',
        name: 'ManagerDashboard',
        meta: { requiresAuth: true, requiresManager: true }, // Защита для менеджера,
        children: [
            {
                path: 'users',
                name: 'EditUsers',
                component: UserManagement,
            },
            {
                path: 'customers',
                name: 'EditCustomers',
                component: CustomerManagement,
            },
            {
                path: 'products',
                name: 'ProductManagement',
                component: ProductManagement,
            },
            {
                path: 'orders',
                name: 'OrderManagement',
                component: OrderManagement,
            },
        ],
        
    },
    {
        path: '/customer',
        name: 'CustomerDashboard',
        meta: { requiresAuth: true },
        children: [
            {
                path: 'products',
                name: 'ProductList',
                component: ProductList,
            },
            {
                path: 'cart',
                name: 'Cart',
                component: Cart,
            },
            {
                path: 'orders',
                name: 'OrderList',
                component: OrderList,
            },
        ],
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

// Охрана маршрутов
router.beforeEach((to, from, next) => {
    const isAuthenticated = store.getters.isAuthenticated;

    if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated) {
        // Если маршрут требует авторизации, но пользователь не авторизован
        next({ name: 'Login' }); // Перенаправляем на страницу логина
    } else if (to.matched.some(record => record.meta.requiresManager) && (!isAuthenticated || !store.getters.isManager)) {
        // Если маршрут требует роль менеджера, но пользователь не менеджер
        next({ name: 'Login' }); // Перенаправляем на страницу логина
    } else {
        next(); // Продолжаем переход
    }
});

export default router;
