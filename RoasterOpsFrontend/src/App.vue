<template>
    <div id="app">
    <nav>
        <router-link to="/inventory">Inventory</router-link> |
    <router-link to="/orders">Orders</router-link>
    </nav>
    <router-view />
    </div>
    </template>

    <style scoped>
nav {
  margin-bottom: 20px;
}
</style>
