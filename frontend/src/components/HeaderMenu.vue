<template>
  <header class="header-menu" v-if="isAuthenticated">
    <nav>
      <ul class="menu">
        <li v-if="role === 'Заказчик'">
          <router-link to="/customer/products">Товары</router-link>
        </li>
        <li v-if="role === 'Заказчик'">
          <router-link to="/customer/orders">Заказы</router-link>
        </li>
        <li v-if="role === 'Заказчик'">
          <router-link to="/customer/cart">Корзина</router-link>
        </li>
        <li v-if="role === 'Менеджер'">
          <router-link to="/manager/products">Редактирование товаров</router-link>
        </li>
        <li v-if="role === 'Менеджер'">
          <router-link to="/manager/orders">Редактирование заказов</router-link>
        </li>
        <li v-if="role === 'Менеджер'">
          <router-link to="/manager/customers">Редактирование заказчиков</router-link>
        </li>
        <li v-if="role === 'Менеджер'">
          <router-link to="/manager/users">Редактирование пользователей</router-link>
        </li>
      </ul>
    </nav>
    <div class="user-info">
      <span>{{ userName }} ({{ role }})</span>
      <button @click="logout" class="logout-button">Выйти</button>
    </div>
  </header>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
  computed: {
    ...mapGetters(['isAuthenticated', 'isManager', 'user']),
    userName() {
      return this.user ? this.user.email : 'Неизвестно';
    },
    role() {
      return this.isManager ? 'Менеджер' : 'Заказчик';
    },
  },
  methods: {
    ...mapActions(['logoutFromApp']),
    async logout() {
      await this.logoutFromApp();
      this.$router.push('/login');
    },
  },
};
</script>

<style scoped>
.header-menu {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 1rem 2rem;
  background-color: #ffffff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.menu {
  list-style: none;
  display: flex;
  gap: 1.5rem;
}

.menu li a {
  text-decoration: none;
  color: #333;
  font-weight: 500;
  padding: 0.5rem 0;
  transition: color 0.3s;
}

.menu li a:hover {
  color: #4CAF50;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 1rem;
}

.logout-button {
  background-color: #f44336;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.logout-button:hover {
  background-color: #d32f2f;
}
</style>
