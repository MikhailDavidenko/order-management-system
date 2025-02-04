<template>
  <div class="modal-overlay" v-if="isVisible">
    <div class="dialog">
      <h2>{{ customer ? 'Редактировать заказчика' : 'Создать заказчика' }}</h2>
      <form @submit.prevent="submit">
        <div class="form-group">
          <label for="name">Наименование:</label>
          <input type="text" v-model="formData.name" required />
        </div>
        <div class="form-group" v-if="customer">
          <label for="code">Код:</label>
          <input type="text" v-model="formData.code" required />
        </div>
        <div class="form-group">
          <label for="address">Адрес:</label>
          <input type="text" v-model="formData.address" />
        </div>
        <div class="form-group">
          <label for="discount">Скидка (%):</label>
          <input type="number" v-model="formData.discount" min="0" max="100" />
        </div>
        <div class="dialog-actions">
          <button type="submit">{{ customer ? 'Сохранить' : 'Создать' }}</button>
          <button type="button" @click="close">Отмена</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    customer: {
      type: Object,
      default: null,
    },
    isVisible: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      formData: {
        name: this.customer ? this.customer.name : '',
        code: this.customer ? this.customer.code : '',
        address: this.customer ? this.customer.address : '',
        discount: this.customer ? this.customer.discount : null,
      },
    };
  },
  methods: {
    submit() {
      this.$emit('submit', { ...this.formData, id: this.customer ? this.customer.id : undefined });
    },
    close() {
      this.$emit('close');
    },
  },
  watch: {
    customer: {
      immediate: true,
      handler(newCustomer) {
        if (newCustomer) {
          this.formData = {
            name: newCustomer.name,
            code: newCustomer.code,
            address: newCustomer.address,
            discount: newCustomer.discount,
          };
        } else {
          this.formData = {
            name: '',
            code: '',
            address: '',
            discount: null,
          };
        }
      },
    },
  },
};
</script>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.dialog {
  border: 1px solid #ccc;
  padding: 20px;
  background: #fff;
  border-radius: 8px;
  width: 400px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

h2 {
  margin-bottom: 15px;
  font-size: 1.5em;
  color: #333;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
}

input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  transition: border-color 0.3s;
}

input:focus {
  border-color: #007bff;
}

.dialog-actions {
  margin-top: 20px;
}

.dialog-actions button {
  margin-right: 10px;
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.dialog-actions button[type="submit"] {
  background-color: #28a745;
  color: white;
}

.dialog-actions button[type="submit"]:hover {
  background-color: #218838;
}

.dialog-actions button[type="button"] {
  background-color: #dc3545;
  color: white;
}

.dialog-actions button[type="button"]:hover {
  background-color: #c82333;
}
</style>
