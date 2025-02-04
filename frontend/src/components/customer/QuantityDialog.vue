<template>
  <div class="dialog-overlay" @click.self="close">
    <div class="dialog">
      <h2>Добавить в корзину</h2>
      <p>Товар: {{ product.name }}</p>
      <label for="quantity">Количество:</label>
      <input type="number" id="quantity" v-model="localQuantity" min="1" />
      <div class="dialog-actions">
        <button class="confirm-button" @click="confirm">Добавить</button>
        <button class="cancel-button" @click="close">Отмена</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    product: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      localQuantity: 1, // Локальная переменная для хранения количества
    };
  },
  methods: {
    confirm() {
      console.log(this.localQuantity);
      this.$emit('confirm', this.localQuantity); // Эмитируем локальное количество
    },
    close() {
      this.$emit('close');
    },
  },
};
</script>

<style scoped>
.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.7); /* Темнее для большего контраста */
  display: flex;
  justify-content: center;
  align-items: center;
  animation: fadeIn 0.3s ease; /* Анимация появления */
}

.dialog {
  background-color: #fff;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.2);
  max-width: 400px;
  width: 100%;
  animation: slideIn 0.3s ease; /* Анимация слайда */
}

h2 {
  margin: 0 0 15px;
  color: #333;
  font-size: 1.8em;
  text-align: center;
}

label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  color: #555;
}

input[type="number"] {
  width: 100%;
  padding: 10px;
  margin-bottom: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 1em;
}

.dialog-actions {
  display: flex;
  justify-content: space-between;
}

.confirm-button,
.cancel-button {
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1em;
  transition: background-color 0.3s, transform 0.2s;
}

.confirm-button {
  background-color: #28a745; /* Зеленый цвет для подтверждения */
  color: white;
}

.confirm-button:hover {
  background-color: #218838; /* Темнее при наведении */
  transform: scale(1.05);
}

.cancel-button {
  background-color: #dc3545; /* Красный цвет для отмены */
  color: white;
}

.cancel-button:hover {
  background-color: #c82333; /* Темнее при наведении */
  transform: scale(1.05);
}

/* Анимации */
@keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

@keyframes slideIn {
  from {
    transform: translateY(-20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}
</style>
