<template>
  <div class="order-management">
    <h1>Управление заказами</h1>

    <div>
      <label for="statusFilter">Фильтр по статусу:</label>
      <select v-model="selectedStatus" @change="fetchOrders">
        <option value="">Все статусы</option>
        <option value="New">Новый</option>
        <option value="InProgress">Выполняется</option>
        <option value="Completed">Выполнен</option>
      </select>
    </div>

    <div class="order-list">
      <OrderItem
          v-for="order in orders"
          :key="order.id"
          :order="order"
          @confirm-order="confirmOrder"
          @close-order="closeOrder"
      />
    </div>

    <Pagination
        :total="totalOrders"
        :limit="limit"
        :offset="offset"
        @page-changed="changePage"
    />
  </div>
</template>

<script>
import axios from 'axios';
import OrderItem from './OrderItem.vue';
import Pagination from './../Pagination.vue'; // Путь к вашему компоненту пагинации

export default {
  components: {
    OrderItem,
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
  methods: {
    async fetchOrders() {
      try {
        const response = await axios.get(`http://localhost:5026/api/v1/orders`, {
          params: {
            offset: this.offset,
            limit: this.limit,
            status: this.selectedStatus || null,
          },
        });

        this.orders = response.data;

        const totalResponse = await axios.get(`http://localhost:5026/api/v1/orders/count`, {
          params: { status: this.selectedStatus || null },
        });
        this.totalOrders = totalResponse.data;
      } catch (error) {
        console.error('Ошибка при загрузке заказов:', error);
      }
    },
    changePage(newOffset) {
      this.offset = newOffset;
      this.fetchOrders();
    },
    async confirmOrder(orderId) {
      try {
        await axios.put(`http://localhost:5026/api/v1/orders/${orderId}/confirm`, {
          shipmentDate: new Date(),
        });
        await this.fetchOrders();
      } catch (error) {
        console.error('Ошибка при подтверждении заказа:', error);
      }
    },
    async closeOrder(orderId) {
      try {
        await axios.put(`http://localhost:5026/api/v1/orders/${orderId}/close`);
        await this.fetchOrders();
      } catch (error) {
        console.error('Ошибка при закрытии заказа:', error);
      }
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
  background-color: #f8f9fa;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

h1 {
  color: #333;
  margin-bottom: 20px;
  text-align: center;
}

.order-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.order-item {
  flex: 0 1 calc(33.33% - 20px);
  margin: 10px; 
  box-sizing: border-box; 
}

@media (max-width: 768px) {
  .order-item {
    flex: 0 1 calc(50% - 20px);
  }
}

@media (max-width: 480px) {
  .order-item {
    flex: 0 1 100%;
  }
}
</style>
