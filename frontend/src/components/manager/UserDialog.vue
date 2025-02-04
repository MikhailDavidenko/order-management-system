<template>
  <div class="dialog">
    <div class="dialog-content">
      <h2>{{ isEdit ? 'Редактировать пользователя' : 'Создать пользователя' }}</h2>
      <form @submit.prevent="submitForm">
        <label for="email">Email:</label>
        <input type="email" v-model="formData.email" required />

        <label for="password" v-if="!isEdit">Пароль:</label>
        <input type="password" v-model="formData.password" v-if="!isEdit" required />

        <label for="roleName">Роль:</label>
        <select v-model="formData.roleName" required>
          <option value="Заказчик">Заказчик</option>
          <option value="Менеджер">Менеджер</option>
        </select>

        <label for="customerId">Заказчик:</label>
        <select v-model="formData.customerId" required>
          <option value="" disabled>Выберите заказчика</option>
          <option v-for="customer in customers" :key="customer.id" :value="customer.id">
            {{ customer.name }}
          </option>
        </select>

        <div class="button-group">
          <button type="submit" class="btn">{{ isEdit ? 'Сохранить' : 'Создать' }}</button>
          <button type="button" class="btn cancel" @click="$emit('close')">Отмена</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    user: {
      type: Object,
      default: () => null,
    },
    customers: {
      type: Array,
      default: () => [],
    },
  },
  data() {
    return {
      formData: {
        email: this.user ? this.user.email : '',
        password: '',
        roleName: this.user ? this.user.roleName : '',
        customerId: this.user ? this.user.customerId : '',
      },
    };
  },
  computed: {
    isEdit() {
      return !!this.user;
    },
  },
  methods: {
    submitForm() {
      const userData = {
        email: this.formData.email,
        roleName: this.formData.roleName,
        customerId: this.formData.customerId,
      };

      if (this.isEdit) {
        userData.id = this.user.id;
      } else {
        userData.password = this.formData.password;
      }

      this.$emit('submit', userData);
    },
  },
};
</script>

<style scoped>
.dialog {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.dialog-content {
  background: #fff;
  padding: 20px;
  border-radius: 8px;
  width: 400px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.3);
}

h2 {
  margin-bottom: 20px;
  color: #333;
  text-align: center;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  color: #555;
}

input, select {
  width: 100%;
  padding: 10px;
  margin-bottom: 15px;
  border: 1px solid #ccc;
  border-radius: 4px;
  transition: border-color 0.3s;
}

input:focus, select:focus {
  border-color: #007bff;
  outline: none;
}

.button-group {
  display: flex;
  justify-content: space-between;
}

.btn {
  background-color: #007bff;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
  flex: 1;
  margin-right: 10px;
}

.btn:last-child {
  margin-right: 0;
}

.btn:hover {
  background-color: #0056b3;
}

.btn.cancel {
  background-color: #dc3545;
}

.btn.cancel:hover {
  background-color: #c82333;
}
</style>
