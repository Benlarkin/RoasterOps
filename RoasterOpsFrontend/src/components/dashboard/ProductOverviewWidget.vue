<script setup>
import { ref, watch, onMounted } from "vue";
import { ProductApiClient } from "../../services/ProductApiClient.ts";

const service = new ProductApiClient();
const products = ref([]);
const filteredProducts = ref([]);
const selectedProduct = ref(null);
const searchQuery = ref("");
const loading = ref(false);
const error = ref("");

const fetchProducts = async () => {
  loading.value = true;
  error.value = "";
  try {
    const items = await service.getProducts();
    products.value = filteredProducts.value = items;
  } catch (err) {
    error.value = "Failed to load products.";
    console.error(err);
  } finally {
    loading.value = false;
  }
};

const searchProducts = () => {
  const q = searchQuery.value.toLowerCase();
  filteredProducts.value = products.value.filter(
      (p) =>
          p.name.toLowerCase().includes(q) ||
          p.type.toLowerCase().includes(q) ||
          p.unit.toLowerCase().includes(q)
  );
};

watch(searchQuery, searchProducts);
onMounted(fetchProducts);
</script>

<template>
  <div class="layout-card">
    <div class="products-header">
      <span class="products-title">Products Overview</span>
      <IconField class="search-field">
        <InputIcon class="pi pi-search" />
        <InputText
            v-model="searchQuery"
            placeholder="Search products..."
            class="products-search"
            @keyup.enter="searchProducts"
        />
      </IconField>
    </div>

    <div v-if="error" class="text-red-500 mb-2">{{ error }}</div>

    <div class="products-table-container">
      <DataTable
          :value="filteredProducts"
          v-model:selection="selectedProduct"
          selectionMode="single"
          :loading="loading"
          :rows="5"
          class="products-table"
          :pt="{
          mask: { class: 'products-table-mask' },
          loadingIcon: { class: 'products-table-loading' }
        }"
      >
        <Column field="name" header="Name" sortable />
        <Column field="type" header="Type" sortable />
        <Column field="unit" header="Unit" sortable />
        <Column field="stockLevel" header="Stock Level" sortable>
          <template #body="{ data }">{{ data.stockLevel }}</template>
        </Column>
      </DataTable>
    </div>
  </div>
</template>
