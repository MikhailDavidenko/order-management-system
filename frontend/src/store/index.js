import { createStore } from 'vuex';
import axios from 'axios';

axios.defaults.withCredentials = true;

export default createStore({
    state: {
        user: JSON.parse(localStorage.getItem('user')) || null, // Загружаем пользователя из localStorage
        products: [],
        orders: [],
        cartItems: JSON.parse(localStorage.getItem('cartItems')) || [], // Загружаем корзину из localStorage
    },
    mutations: {
        setUser (state, user) {
            state.user = user;
            localStorage.setItem('user', JSON.stringify(user)); // Сохраняем пользователя в localStorage
        },
        clearUser (state) {
            state.user = null;
            localStorage.removeItem('user'); // Удаляем пользователя из localStorage
        },
        setProducts(state, products) {
            state.products = products;
        },
        setOrders(state, orders) {
            state.orders = orders;
        },
        ADD_TO_CART(state, item) {
            const existingItem = state.cartItems.find(cartItem => cartItem.product.id === item.product.id);
            if (existingItem) {
                existingItem.quantity += item.quantity; // Увеличиваем количество, если товар уже в корзине
            } else {
                state.cartItems.push(item); // Добавляем новый товар в корзину
            }
            localStorage.setItem('cartItems', JSON.stringify(state.cartItems)); // Сохраняем корзину в localStorage
        },
        CLEAR_CART(state) {
            state.cartItems = []; // Очищаем корзину
            localStorage.removeItem('cartItems'); // Удаляем корзину из localStorage
        },
    },
    actions: {
        async login({ commit }, credentials) {
            const userId = await axios.post('http://localhost:5026/Auth/login', credentials);
            const user = await axios.get(`http://localhost:5026/api/v1/Users/${userId.data}`);
            commit('setUser', user.data);
            return user.data;
        },
        async logoutFromApp({ commit }) {
            await axios.post('http://localhost:5026/Auth/logout');
            commit('clearUser'); // Удаляем пользователя
        },
        async fetchProducts({ commit }) {
            const response = await axios.get('/api/products');
            commit('setProducts', response.data);
        },
        async fetchOrders({ commit }) {
            const response = await axios.get('/api/orders');
            commit('setOrders', response.data);
        },
        addToCart({ commit }, item) {
            commit('ADD_TO_CART', item); // Вызываем мутацию для добавления товара в корзину
        },
        clearCart({ commit }) {
            commit('CLEAR_CART'); // Вызываем мутацию для очистки корзины
        },
    },
    getters: {
        isAuthenticated(state) {
            return !!state.user;
        },
        isManager(state) {
            return state.user && state.user.roleNames.includes('Менеджер');
        },
        user(state) {
            return state.user;
        },
        cartItems(state) {
            return state.cartItems; // Геттер для получения товаров в корзине
        },
        cartTotal(state) {
            return state.cartItems.reduce((total, item) => {
                return total + (item.product.priceWithDiscount * item.quantity);
            }, 0); // Подсчет общей суммы в корзине
        },
    },
});
