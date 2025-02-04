<template>
  <div class="order-item">
    <h3>Заказ #{{ order.orderNumber }}</h3>
    <p>Статус: {{ order.status }}</p>
    <p>Сумма: {{ order.totalPrice }} ₽</p>
    <button v-if="order.status === 'New'" @click="confirmOrder" class="btn confirm">Подтвердить заказ</button>
    <button v-if="order.status === 'InProgress'" @click="closeOrder" class="btn close">Закрыть заказ</button>
  </div>
</template>

<script>
export default {
  props: {
    order: {
      type: Object,
      required: true,
    },
  },
  methods: {
    confirmOrder() {
      this.$emit('confirm-order', this.order.id);
    },
    closeOrder() {
      this.$emit('close-order', this.order.id);
    },
  },
};
</script>

<style scoped>
.order-item {
  padding: 15px;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: #ffffff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

h3 {
  margin: 0 0 10px;
}

.btn {
  margin-top: 10px;
  background-color: #0e5db3;
  color: white;
  padding: 8px 12px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.btn:hover {
  background-color: #0b4a8e;
}

.confirm {
  background-color: #218334;
}

.confirm:hover {
  background-color: #1a6f2c;
}

.close {
  background-color: #bd1424;
}

.close:hover {
  background-color: #a10e1d;
}
</style>