<template>
  <div class="layout-card col-item-2">
    <span class="order-title">Order Form</span>
    <form @submit.prevent="submitOrder" class="order-form">
    <div class="p-fluid p-formgrid p-grid">
      <div class="p-field p-col-12 p-md-6">
        <label for="customerName">Customer Name</label>
        <InputText id="customerName" v-model="customerName" required />
      </div>
    </div>

    <div class="line-items">
      <div
          v-for="(item, idx) in lineItems"
          :key="idx"
          class="p-fluid p-formgrid p-grid line-item-row"
      >
        <div class="p-field p-col-6 p-md-5 add-btn-spacing">
          <label :for="`product-${idx}`">Product</label>
          <Dropdown
              :id="`product-${idx}`"
              v-model="item.productId"
              :options="products"
              optionLabel="name"
              optionValue="productId"
              placeholder="Select a product"
              required
          />
        </div>
        <div class="p-field p-col-4 p-md-5">
          <label :for="`qty-${idx}`">Quantity</label>
          <InputNumber
              :id="`qty-${idx}`"
              v-model.number="item.quantity"
              :min="1"
              show-buttons
              required
          />
        </div>

      </div>
      <Button
          type="button"
          label="Add Item"
          icon="pi pi-plus"
          class="p-button-success add-btn-spacing"
          @click.prevent="addLine"
      />
        <Button
            type="button"
            icon="pi pi-trash"
            class="p-button-rounded p-button-danger"
            @click.prevent="removeLine(idx)"
        />
    </div>

    <div class="p-mt-4">
      <Button type="submit" label="Submit Order" icon="pi pi-check" class="p-button-primary" />
    </div>
  </form>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import axios from 'axios'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import InputNumber from 'primevue/inputnumber'
import Button from 'primevue/button'
import type { Product } from '@/types'

interface LineItemInput {
  productId: number | null
  quantity: number | null
}

const products = ref<Product[]>([])
const customerName = ref<string>('')
const lineItems = ref<LineItemInput[]>([
  { productId: null, quantity: null }
])

onMounted(async () => {
  const res = await axios.get<Product[]>('https://localhost:7297/api/products')
  products.value = res.data
})

function addLine() {
  lineItems.value.push({ productId: null, quantity: null })
}

function removeLine(index: number) {
  if (lineItems.value.length > 1) {
    lineItems.value.splice(index, 1)
  }
}

async function submitOrder() {
  const now = new Date().toISOString()

  const payload = {
    orderId:      0,
    customerName: customerName.value,
    orderDate:    now,
    status:       0,   // Pending
    lineItems: lineItems.value.map(li => ({
      orderLineItemId: 0,
      orderId:         0,
      productId:       li.productId,
      quantity:        li.quantity
    }))
  }

  await axios.post('https://localhost:7297/api/orders', payload)

  // reset form
  customerName.value = ''
  lineItems.value = [{ productId: null, quantity: null }]
}

</script>

<style scoped>
.order-form {
  max-width: 800px;
  margin: 0 auto;
}
.add-btn-spacing {
  margin-bottom: 1rem;
  margin-right: 1rem;
}
.p-field label {
  display: block;
  margin-bottom: .75rem;
}
.line-items {
  margin-top: 1rem;
}
.line-item-row {
  align-items: center;
  margin-bottom: 1rem;
}
.remove-btn-col {
  display: flex;
  align-items: flex-end;
  justify-content: center;
}
.order-title {
  font-weight: bold;
}
</style>
