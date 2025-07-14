<script setup>
import AppTopbar from "./components/AppTopbar.vue";
import AppFooter from "./components/AppFooter.vue";
import ProductOverviewWidget from "./components/dashboard/ProductOverviewWidget.vue";
import SalesTrendWidget from "./components/dashboard/SalesTrendWidget.vue";
</script>

<template>
    <div class="layout-container">
        <AppTopbar />
        <div class="layout-grid">
            <ProductOverviewWidget />
          <SalesTrendWidget />
        </div>
        <AppFooter />
    </div>
</template>
