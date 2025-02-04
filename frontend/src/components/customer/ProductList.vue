<template>
  <div>
    <h1>Каталог товаров</h1>
    <div class="product-grid">
      <ProductItem
          v-for="product in products"
          :key="product.id"
          :product="product"
          @add-to-cart="openQuantityDialog(product)"
      />
    </div>

    <Pagination
        :total="totalProducts"
        :limit="limit"
        :offset="offset"
        @page-changed="fetchProducts"
    />

    <QuantityDialog
        v-if="showDialog"
        :product="selectedProduct"
        :quantity="quantity"
        @close="showDialog = false"
        @confirm="toCart"
    />
  </div>
</template>

<script>
import axios from 'axios';
import ProductItem from './ProductItem.vue';
import Pagination from './../Pagination.vue';
import QuantityDialog from './QuantityDialog.vue';
import { mapActions } from 'vuex';

export default {
  components: {
    ProductItem,
    Pagination,
    QuantityDialog,
  },
  data() {
    return {
      products: [],
      totalProducts: 0,
      limit: 12,
      offset: 0,
      showDialog: false,
      selectedProduct: null,
      quantity: 1,
    };
  },
  methods: {
    ...mapActions(['addToCart']),

    async fetchProducts(newOffset = 0) {
      this.offset = newOffset;
      try {
        const response = await axios.get(`http://localhost:5026/api/v1/products`, {
          params: { offset: this.offset, limit: this.limit },
        });
        this.products = response.data;

        const totalResponse = await axios.get(`http://localhost:5026/api/v1/products/count`);
        this.totalProducts = totalResponse.data;
      } catch (error) {
        console.error('Ошибка при загрузке товаров:', error);
      }
    },
    openQuantityDialog(product) {
      this.selectedProduct = product;
      this.quantity = 1; // Сбрасываем количество
      this.showDialog = true;
    },
    async toCart(localQuantity) {
      try {
        const item = {
          product: this.selectedProduct,
          quantity: localQuantity,
        };

        this.$store.dispatch('addToCart', item);
        this.showDialog = false;
      } catch (error) {
        console.error('Ошибка при добавлении товара в корзину:', error);
      }
    },
  },
  mounted() {
    this.fetchProducts();
  },
};
</script>

<style scoped>
.product-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 20px;
  margin-top: 20px;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
  color: #333;
  font-size: 2em;
  font-weight: bold;
}
</style>
