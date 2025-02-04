<template>
  <div class="product-management">
    <h1>Управление товарами</h1>
    <button @click="openCreateProductDialog" class="btn add-product">Создать товар</button>
    <div class="product-list">
      <div class="product-item" v-for="product in products" :key="product.id">
        <h3>{{ product.name }}</h3>
        <p>Код: {{ product.code }}</p>
        <p>Цена: {{ product.price }} ₽</p>
        <p v-if="product.discount">Цена со скидкой: {{ product.priceWithDiscount }} ₽</p>
        <p v-if="product.discount">Скидка: {{ product.discount }}%</p>
        <button @click="openEditProductDialog(product)" class="btn edit">Редактировать</button>
        <button @click="deleteProduct(product.id)" class="btn delete">Удалить</button>
      </div>
    </div>

    <ProductDialog
        v-if="isCreateProductDialogOpen"
        @close="closeCreateProductDialog"
        @submit="createProduct"
        :is-visible="true"
    />

    <ProductDialog
        v-if="isEditProductDialogOpen"
        :product="selectedProduct"
        @close="closeEditProductDialog"
        @submit="updateProduct"
        :is-visible="true"
    />
  </div>
</template>

<script>
import axios from 'axios';
import ProductDialog from './ProductDialog.vue';

export default {
  components: {
    ProductDialog,
  },
  data() {
    return {
      products: [],
      isCreateProductDialogOpen: false,
      isEditProductDialogOpen: false,
      selectedProduct: null,
    };
  },
  methods: {
    async fetchProducts() {
      const response = await axios.get('http://localhost:5026/api/v1/products');
      this.products = response.data;
    },
    openCreateProductDialog() {
      this.isCreateProductDialogOpen = true;
    },
    closeCreateProductDialog() {
      this.isCreateProductDialogOpen = false;
    },
    openEditProductDialog(product) {
      this.selectedProduct = product;
      this.isEditProductDialogOpen = true;
    },
    closeEditProductDialog() {
      this.isEditProductDialogOpen = false;
      this.selectedProduct = null;
    },
    async createProduct(productData) {
      await axios.post('http://localhost:5026/api/v1/products', productData);
      await this.fetchProducts();
      this.closeCreateProductDialog();
    },
    async updateProduct(productData) {
      await axios.put(`http://localhost:5026/api/v1/products/${productData.id}`, productData);
      await this.fetchProducts();
      this.closeEditProductDialog();
    },
    async deleteProduct(productId) {
      await axios.delete(`http://localhost:5026/api/v1/products/${productId}`);
      await this.fetchProducts();
    },
  },
  mounted() {
    this.fetchProducts();
  },
};
</script>

<style scoped>
.product-management {
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

.product-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.product-item {
  flex: 0 1 calc(33.33% - 20px);
  margin: 10px;
  padding: 15px;
  border: 1px solid #ccc;
  border-radius: 5px;
  background-color: #ffffff;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  box-sizing: border-box;
}

.product-item h3 {
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

edit {
  background-color: #0e5db3;
}

.edit:hover {
  background-color: #0b4a8e;
}

.delete {
  background-color: #bd1424;
}

.delete:hover {
  background-color: #a10e1d;
}

.add-product {
  margin-bottom: 20px;
  background-color: #218334;
}

.add-product:hover {
  background-color: #1a6f2c;
}

@media (max-width: 768px) {
  .product-item {
    flex: 0 1 calc(50% - 20px);
  }
}

@media (max-width: 480px) {
  .product-item {
    flex: 0 1 100%;
  }
}
</style>
