<script setup>
import AppTopbar from "./components/AppTopbar.vue";
import AppFooter from "./components/AppFooter.vue";
import ProductOverviewWidget from "./components/dashboard/ProductOverviewWidget.vue";
</script>

<template>
    <div class="layout-container">
        <AppTopbar />
        <div class="layout-grid">
            <ProductOverviewWidget />
        </div>
        <AppFooter />
    </div>
</template>
