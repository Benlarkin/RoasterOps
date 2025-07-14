<script setup>
import AppTopbar from "./components/AppTopbar.vue";
import AppFooter from "./components/AppFooter.vue";
import ProductOverviewWidget from "./components/dashboard/ProductOverviewWidget.vue";
import SalesTrendWidget from "./components/dashboard/SalesTrendWidget.vue";
import OrderForm from "./components/dashboard/OrderForm.vue";
</script>

<template>
    <div class="layout-container">
        <AppTopbar />
        <div class="layout-grid">
            <ProductOverviewWidget />
          <div class="layout-grid-row">
            <SalesTrendWidget />
            <OrderForm />
          </div>
        </div>
        <AppFooter />
    </div>
</template>
