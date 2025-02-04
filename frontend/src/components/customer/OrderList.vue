<template>
  <div class="order-management">
    <h2>Список заказов</h2>

    <div class="filter">
      <label for="statusFilter">Фильтр по статусу:</label>
      <select v-model="selectedStatus" @change="fetchOrders">
        <option value="">Все статусы</option>
        <option value="New">Новый</option>
        <option value="InProgress">Выполняется</option>
        <option value="Completed">Выполнен</option>
      </select>
    </div>

    <ul class="order-list">
      <li v-for="order in orders" :key="order.id" class="order-item">
        <p><strong>Номер заказа:</strong> {{ order.orderNumber }}</p>
        <p><strong>Дата заказа:</strong> {{ order.orderDate }}</p>
        <p><strong>Дата доставки:</strong> {{ order.shipmentDate || '-' }}</p>
        <p><strong>Состояние:</strong> {{ order.status }}</p>
        <p><strong>Сумма:</strong> {{ order.totalPrice }} ₽</p>
        <button v-if="order.status == 'New'" @click="deleteOrder(order.id)" class="btn delete">Удалить заказ</button>
      </li>
    </ul>

    <Pagination
        :total="totalOrders"
        :limit="limit"
        :offset="offset"
        @page-changed="changePage"
    />
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import Pagination from './../Pagination.vue';
import axios from "axios";

export default {
  components: {
    Pagination,
  },
  data() {
    return {
      orders: [],
      totalOrders: 0,
      limit: 12,
      offset: 0,
      selectedStatus: '',
    };
  },
  computed: {
    ...mapGetters(['user']),
  },
  methods: {
    async fetchOrders() {
      try {
        const response = await axios.get(`http://localhost:5026/api/v1/orders`, {
          params: { offset: this.offset, limit: this.limit, status: this.selectedStatus || null },
        });

        this.orders = response.data;

        const totalResponse = await axios.get(`http://localhost:5026/api/v1/orders/count`);
        this.totalOrders = totalResponse.data;
      } catch (error) {
        console.error('Ошибка при загрузке заказов:', error);
      }
    },
    async deleteOrder(orderId) {
      try {
        await axios.delete(`http://localhost:5026/api/v1/orders/${orderId}`);
        await this.fetchOrders();
      } catch (error) {
        console.error('Ошибка при удалении заказа:', error);
      }
    },
    changePage(newOffset) {
      this.offset = newOffset;
      this.fetchOrders();
    },
  },
  mounted() {
    this.fetchOrders();
  },
};
</script>

<style scoped>
.order-management {
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

h2 {
  color: #333;
  margin-bottom: 20px;
  text-align: center;
}

.filter {
  margin-bottom: 20px;
  display: flex;
  justify-content: center;
  align-items: center;
}

.filter label {
  margin-right: 10px;
}

.order-list {
  list-style-type: none;
  padding: 0;
}

.order-item {
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 5px;
  padding: 15px;
  margin-bottom: 10px;
  transition: box-shadow 0.3s;
}

.order-item:hover {
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.2);
}

.order-item p {
  margin: 5px 0;
}

.order-item strong {
  color: #555;
}

.btn.delete {
  background-color: #bd1424;
  color: white;
  border: none;
  border-radius: 4px;
  padding: 8px 12px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn.delete:hover {
  background-color: #a10e1d;
}

.pagination {
  display: flex;
  justify-content: center;
  margin-top: 20px;
}
</style>