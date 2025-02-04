<template>
  <div class="modal-overlay" v-if="isVisible">
    <div class="dialog">
      <h2>{{ product ? 'Редактировать товар' : 'Создать товар' }}</h2>
      <form @submit.prevent="submit">
        <div>
          <label for="name">Наименование:</label>
          <input type="text" v-model="formData.name" required />
        </div>
        <div v-if="product">
          <label for="code">Код:</label>
          <input type="text" v-model="formData.code" required />
        </div>
        <div>
          <label for="price">Цена:</label>
          <input type="number" v-model="formData.price" required />
        </div>
        <div>
          <label for="category">Категория:</label>
          <input type="text" v-model="formData.category" />
        </div>
        <div class="dialog-actions">
          <button type="submit" class="btn submit">{{ product ? 'Сохранить' : 'Создать' }}</button>
          <button type="button" class="btn cancel" @click="close">Отмена</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    product: {
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
        name: this.product ? this.product.name : '',
        code: this.product ? this.product.code : '',
        price: this.product ? this.product.price : null,
        category: this.product ? this.product.category : '',
      },
    };
  },
  methods: {
    submit() {
      this.$emit('submit', { ...this.formData, id: this.product ? this.product.id : undefined });
    },
    close() {
      this.$emit('close');
    },
  },
  watch: {
    product: {
      immediate: true,
      handler(newProduct) {
        if (newProduct) {
          this.formData = {
            name: newProduct.name,
            code: newProduct.code,
            price: newProduct.price,
            category: newProduct.category,
          };
        } else {
          this.formData = {
            name: '',
            code: '',
            price: null,
            category: '',
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
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.dialog {
  border: 1px solid #ccc;
  padding: 20px;
  background: #fff;
  border-radius: 5px;
  width: 400px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.dialog-actions {
  margin-top: 20px;
}

.dialog-actions .btn {
  margin-right: 10px;
  padding: 8px 12px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.dialog-actions .btn.submit {
  background-color: #0e5db3;
  color: white;
}

.dialog-actions .btn.submit:hover {
  background-color: #0b4a8e;
}

.dialog-actions .btn.cancel {
  background-color: #bd1424;
  color: white;
}

.dialog-actions .btn.cancel:hover {
  background-color: #a10e1d;
}

input {
  width: 100%;
  padding: 8px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

input:focus {
  border-color: #0e5db3;
  outline: none;
}
</style>
