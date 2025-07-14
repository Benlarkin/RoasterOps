<template>
  <div class="layout-card col-item-2">
    <div class="chart-header">
      <span class="chart-title">Sales Trend</span>
    </div>
    <div class="chart-content">
      <Chart
          v-if="chartData"
          type="bar"
          :data="chartData"
          :options="chartOptions"
          style="height:300px"
      />
      <div v-else class="loading">Loading…</div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch, onMounted } from 'vue'
import axios from 'axios'
import Chart from 'primevue/chart'
import { useLayout } from '../../composables/useLayout.js'
import type { Order } from '@/types'

const { primary, surface, isDarkMode } = useLayout()
const orders = ref<Order[]>([])
const chartData = ref<any>(null)
const chartOptions = ref<any>(null)

async function fetchOrders() {
  const res = await axios.get<Order[]>('https://localhost:7297/api/orders/with-details')
  orders.value = res.data
  updateChart()
}

function updateChart() {
  const docStyle = getComputedStyle(document.documentElement)
  const quarters = ['Q1', 'Q2', 'Q3', 'Q4']
  const productNames = Array.from(
      new Set(
          orders.value.flatMap(o =>
              o.lineItems.map(li => li.product?.name || '')
          )
      )
  ).slice(0, 3)

  const colors = [
    docStyle.getPropertyValue('--p-primary-400'),
    docStyle.getPropertyValue('--p-primary-300'),
    docStyle.getPropertyValue('--p-primary-200')
  ]

  chartData.value = {
    labels: quarters,
    datasets: productNames.map((name, idx) => ({
      label: name,
      backgroundColor: colors[idx] || colors[0],
      data: quarters.map((_, qIdx) => {
        const start = qIdx * 3 + 1
        const end = start + 2
        return orders.value.filter(o => {
          const m = new Date(o.orderDate).getMonth() + 1
          return m >= start && m <= end
              && o.lineItems.some(li => li.product?.name === name)
        }).length
      }),
      barThickness: 32
    }))
  }

  chartOptions.value = {
    maintainAspectRatio: false,
    responsive: true,
    plugins: { legend: { position: 'top' } },
    scales: {
      x: { stacked: true, grid: { color: 'transparent', borderColor: 'transparent' } },
      y: { stacked: true, beginAtZero: false, ticks: {stepSize: 200}, grid: { color: 'transparent', borderColor: 'transparent', drawTicks: false } }
    }
  }
}

watch([primary, surface, isDarkMode], updateChart)
onMounted(fetchOrders)
</script>

<style scoped>
.layout-card { }
.chart-header { margin-bottom:0.5rem }
.chart-title { font-weight:bold }
.chart-content { position:relative }
.loading { text-align:center; padding:2rem; color:#666 }
</style>
