<template>
  <div class="user-management">
    <h1>Управление пользователями</h1>
    <button @click="openCreateUserDialog" class="btn">Создать пользователя</button>
    <table class="user-table">
      <thead>
      <tr>
        <th>Id</th>
        <th>Email</th>
        <th>Действия</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="user in users" :key="user.id">
        <td>{{ user.id }}</td>
        <td>{{ user.email }}</td>
        <td>
          <button @click="openEditUserDialog(user)" class="btn edit">Редактировать</button>
          <button @click="deleteUser (user.id)" class="btn delete">Удалить</button>
        </td>
      </tr>
      </tbody>
    </table>

    <UserDialog
          v-if="isCreateUserDialogOpen"
          @close="closeCreateUserDialog"
          @submit="createUser "
          :customers="customers"
          :is-visible="true"
    />

    <UserDialog
          v-if="isEditUserDialogOpen"
          :user="selectedUser "
          @close="closeEditUserDialog"
          @submit="updateUser "
          :customers="customers"
          :is-visible="true"
    />
  </div>
</template>

<script>
import axios from 'axios';
import UserDialog from './UserDialog.vue';

export default {
  components: { UserDialog },
  data() {
    return {
      users: [],
      customers: [],
      isCreateUserDialogOpen: false,
        isEditUserDialogOpen: false,
        selectedUser:  null,
  };
  },
  methods: {
    async fetchCustomers() {
      const response = await axios.get('http://localhost:5026/api/v1/customers');
      this.customers = response.data;
    },
    async fetchUsers() {
      const response = await axios.get('http://localhost:5026/api/v1/users');
      this.users = response.data;
    },
    openCreateUserDialog() {
  this.isCreateUserDialogOpen = true;
},
closeCreateUserDialog() {
  this.isCreateUserDialogOpen = false;
},
openEditUserDialog(user) {
  this.selectedUser  = user;
  this.isEditUserDialogOpen = true;
},
closeEditUserDialog() {
  this.isEditUserDialogOpen = false;
  this.selectedUser  = null;
},
async createUser (userData) {
  await axios.post('http://localhost:5026/api/v1/users', userData);
  await this.fetchUsers();
  this.closeCreateUserDialog();
},
async updateUser (userData) {
  await axios.put(`http://localhost:5026/api/v1/users/${userData.id}`, userData);
  await this.fetchUsers();
  this.closeEditUserDialog();
},
async deleteUser (userId) {
  await axios.delete(`http://localhost:5026/api/v1/users/${userId}`);
  await this.fetchUsers();
},
},
mounted() {
  this.fetchUsers();
  this.fetchCustomers();
},
};
</script>

<style scoped>
.user-management {
  padding: 20px;
  background-color: #f8f9fa;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

h1 {
  color: #333;
  margin-bottom: 20px;
}

.btn {
  margin-bottom: 15px;
  background-color: #0e5db3;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn:hover {
  background-color: #0e5db3;
}

.user-table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 20px;
}

.user-table th, .user-table td {
  border: 1px solid #ccc;
  padding: 10px;
  text-align: left;
}

.user-table th {
  background-color: #0e5db3;
  color: white;
}

.edit {
  background-color: #218334;
}

.edit:hover {
  background-color: #218334;
}

.delete {
  background-color: #bd1424;
}

.delete:hover {
  background-color: #bd1424;
}
</style>
