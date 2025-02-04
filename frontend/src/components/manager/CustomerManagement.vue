<template>
  <div>
    <h1>Управление заказчиками</h1>
    <button class="btn create" @click="openCreateCustomerDialog">Создать заказчика</button>
    <table>
      <thead>
      <tr>
        <th>Наименование</th>
        <th>Код</th>
        <th>Адрес</th>
        <th>Скидка (%)</th>
        <th>Действия</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="customer in customers" :key="customer.id">
        <td>{{ customer.name }}</td>
        <td>{{ customer.code }}</td>
        <td>{{ customer.address }}</td>
        <td>{{ customer.discount }}</td>
        <td>
          <button class="btn edit" @click="openEditCustomerDialog(customer)">Редактировать</button>
          <button class="btn delete" @click="deleteCustomer(customer.id)">Удалить</button>
        </td>
      </tr>
      </tbody>
    </table>

    <CustomerDialog
        v-if="isCreateCustomerDialogOpen"
        @close="closeCreateCustomerDialog"
        @submit="createCustomer"
        :is-visible="true"
    />

    <CustomerDialog
        v-if="isEditCustomerDialogOpen"
        :customer="selectedCustomer"
        @close="closeEditCustomerDialog"
        @submit="updateCustomer"
        :is-visible="true"
    />
  </div>
</template>

<script>
import axios from 'axios';
import CustomerDialog from './CustomerDialog.vue';

export default {
  components: {
    CustomerDialog,
  },
  data() {
    return {
      customers: [],
      isCreateCustomerDialogOpen: false,
      isEditCustomerDialogOpen: false,
      selectedCustomer: null,
    };
  },
  methods: {
    async fetchCustomers() {
      const response = await axios.get('http://localhost:5026/api/v1/customers');
      this.customers = response.data;
    },
    openCreateCustomerDialog() {
      this.isCreateCustomerDialogOpen = true;
    },
    closeCreateCustomerDialog() {
      this.isCreateCustomerDialogOpen = false;
    },
    openEditCustomerDialog(customer) {
      this.selectedCustomer = customer;
      this.isEditCustomerDialogOpen = true;
    },
    closeEditCustomerDialog() {
      this.isEditCustomerDialogOpen = false;
      this.selectedCustomer = null;
    },
    async createCustomer(customerData) {
      await axios.post('http://localhost:5026/api/v1/customers', customerData);
      await this.fetchCustomers();
      this.closeCreateCustomerDialog();
    },
    async updateCustomer(customerData) {
      await axios.put(`http://localhost:5026/api/v1/customers/${customerData.id}`, customerData);
      await this.fetchCustomers();
      this.closeEditCustomerDialog();
    },
    async deleteCustomer(customerId) {
      await axios.delete(`http://localhost:5026/api/v1/customers/${customerId}`);
      await this.fetchCustomers();
    },
  },
  mounted() {
    this.fetchCustomers();
  },
};
</script>

<style scoped>
h1 {
  text-align: center;
  margin-bottom: 20px;
}

.btn {
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  color: #fff;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn.create {
  background-color: #28a745; /* Зеленый для создания */
}

.btn.create:hover {
  background-color: #218838;
}

.btn.edit {
  background-color: #007bff; /* Синий для редактирования */
}

.btn.edit:hover {
  background-color: #0069d9;
}

.btn.delete {
  background-color: #dc3545; /* Красный для удаления */
}

.btn.delete:hover {
  background-color: #c82333;
}

table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

th, td {
  padding: 10px;
  text-align: left;
  border-bottom: 1px solid #ccc;
}

th {
  background-color: #f8f9fa;
}
</style>
