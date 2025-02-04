<template>
  <div class="pagination">
    <button @click="prevPage" :disabled="offset === 0">Назад</button>
    <span>Страница {{ currentPage }} из {{ totalPages }}</span>
    <button @click="nextPage" :disabled="offset + limit >= total">Вперед</button>
  </div>
</template>

<script>
export default {
  props: {
    total: {
      type: Number,
      required: true,
    },
    limit: {
      type: Number,
      required: true,
    },
    offset: {
      type: Number,
      required: true,
    },
  },
  computed: {
    currentPage() {
      return Math.floor(this.offset / this.limit) + 1;
    },
    totalPages() {
      console.log(this.total, this.limit);
      return Math.ceil(this.total / this.limit);
    },
  },
  methods: {
    prevPage() {
      this.$emit('page-changed', this.offset - this.limit);
    },
    nextPage() {
      this.$emit('page-changed', this.offset + this.limit);
    },
  },
};
</script>

<style scoped>
.pagination {
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 20px 0;
}

button {
  padding: 10px 15px;
  margin: 0 10px;
  border: none;
  border-radius: 5px;
  background-color: #007bff;
  color: white;
  cursor: pointer;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}
</style>
