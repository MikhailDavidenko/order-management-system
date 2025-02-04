<template>
  <div class="product-item">
    <h3>{{ product.name }}</h3>
    <p class="price">
      <span class="old-price">{{ product.price }} ₽</span>
      <span class="discounted-price">{{ product.priceWithDiscount }} ₽</span>
    </p>
    <button class="add-to-cart" @click="$emit('add-to-cart')">В корзину</button>
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
};
</script>

<style scoped>
.product-item {
  border: 1px solid #e0e0e0;
  border-radius: 10px;
  padding: 20px;
  text-align: center;
  transition: transform 0.3s, box-shadow 0.3s;
  background-color: #fff;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
  position: relative;
  overflow: hidden;
}

.product-item:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.15);
}

h3 {
  font-size: 1.5em;
  margin: 10px 0;
  color: #333;
}

.price {
  margin: 15px 0;
}

.old-price {
  text-decoration: line-through;
  color: #999;
  font-size: 1.2em;
}

.discounted-price {
  color: #e74c3c;
  font-weight: bold;
  font-size: 1.5em;
  margin-left: 10px;
}

.add-to-cart {
  background-color: #0e5db3;
  color: white;
  border: none;
  padding: 12px 20px;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s;
  font-size: 1em;
}

.add-to-cart:hover {
  background-color: #0b4a8e;
  transform: scale(1.05);
}
</style>
