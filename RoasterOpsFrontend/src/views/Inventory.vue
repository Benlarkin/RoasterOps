<template>
  <div>
    <h1>Inventory</h1>
    <table>
      <thead>
      <tr>
        <th>Name</th>
        <th>Type</th>
        <th>Unit</th>
        <th>Stock Level</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="product in products" :key="product.productId">
        <td>{{ product.name }}</td>
        <td>{{ product.type }}</td>
        <td>{{ product.unit }}</td>
        <td>{{ product.stockLevel }}</td>
      </tr>
      </tbody>
    </table>

    <form @submit.prevent="addProduct">
      <h3>Add New Product</h3>
      <input v-model="newProduct.name" placeholder="Name" required />
      <select v-model="newProduct.type">
        <option value="Green">Green</option>
        <option value="Roasted">Roasted</option>
      </select>
      <select v-model="newProduct.unit">
        <option value="Bag">Bag</option>
        <option value="Lbs">Lbs</option>
      </select>
      <input v-model.number="newProduct.stockLevel" type="number" placeholder="Stock" required />
      <button type="submit">Add</button>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import axios from 'axios'
import type { Product } from '@/types'

const products = ref<Product[]>([])

const newProduct = ref<Omit<Product, 'productId' | 'createdAt'>>({
  name: '',
  type: 'Green',
  unit: 'Bag',
  stockLevel: 0
})

onMounted(async () => {
  const res = await axios.get<Product[]>('https://localhost:7297/api/products')
  products.value = res.data
})

const addProduct = async () => {
  await axios.post('https://localhost:7297/api/products', newProduct.value)
  const res = await axios.get<Product[]>('https://localhost:7297/api/products')
  products.value = res.data
}
</script>
