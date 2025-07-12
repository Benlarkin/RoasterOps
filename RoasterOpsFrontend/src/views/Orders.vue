<template>
  <div>
    <h1>Orders</h1>
    <div v-if="orders.length === 0">No orders found.</div>

    <div v-else v-for="order in orders" :key="order.orderId" class="order">
      <h3>
        {{ order.customerName }}
        <span style="font-weight: normal;">({{ order.status }})</span>
      </h3>
      <ul>
        <li
            v-for="item in order.lineItems"
            :key="item.orderLineItemId"
        >
          {{ item.product?.name ?? 'Unknown Product' }} — {{ item.quantity }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import axios from 'axios'
import type { Order } from '@/types'

const orders = ref<Order[]>([])

onMounted(async () => {
  try {
    const res = await axios.get<Order[]>('https://localhost:7297/api/orders')
    orders.value = res.data
  } catch (err) {
    console.error('Failed to fetch orders:', err)
  }
})
</script>

<style scoped>
.order {
  margin-bottom: 1rem;
  padding: 1rem;
  border: 1px solid #ddd;
  border-radius: 6px;
  background-color: #f9f9f9;
}
</style>
