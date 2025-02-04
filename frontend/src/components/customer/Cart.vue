<template>
  <div class="cart">
    <h1>Корзина</h1>
    <div v-if="cartItems.length === 0">
      <p>Ваша корзина пуста.</p>
    </div>
    <div v-else>
      <div class="cart-items">
        <div class="cart-item" v-for="item in cartItems" :key="item.product.id">
          <h3>{{ item.product.name }}</h3>
          <p>Цена за единицу: <strong>{{ item.product.priceWithDiscount }} ₽</strong></p>
          <p>Общая цена: <strong>{{ item.product.priceWithDiscount * item.quantity }} ₽</strong></p>
          <p>Количество: {{ item.quantity }}</p>
        </div>
      </div>
      <div class="total">
        <h2>Общая сумма: <strong>{{ totalAmount }} ₽</strong></h2>
        <button @click="placeOrder">Заказать</button>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import axios from 'axios';

export default {
  computed: {
    ...mapGetters(['cartItems', 'cartTotal']),
    totalAmount() {
      return this.cartTotal;
    },
  },
  methods: {
    async placeOrder() {
      try {
        const orderItems = this.cartItems.map(item => ({
          productId: item.product.id,
          quantity: item.quantity,
        }));

        const response = await axios.post('http://localhost:5026/api/v1/orders', {
          items: orderItems,
        });

        console.log('Заказ успешно оформлен:', response.data);
        this.$store.dispatch('clearCart');
      } catch (error) {
        console.error('Ошибка при оформлении заказа:', error);
      }
    },
  },
};
</script>

<style scoped>
.cart {
  padding: 20px;
  max-width: 1200px;
  margin: auto;
}

.cart-items {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.cart-item {
  flex: 0 1 calc(30% - 20px); 
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 15px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  margin-bottom: 20px;
  transition: transform 0.2s;
}

.cart-item:hover {
  transform: translateY(-5px);
}

.total {
  margin-top: 20px;
  text-align: right;
}

button {
  padding: 10px 15px;
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

button:hover {
  background-color: #218838;
}
</style>
