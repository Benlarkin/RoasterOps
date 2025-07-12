<script setup>
import { useLayout } from "../composables/useLayout";
import AppConfig from "./AppConfig.vue";
import logo from "../assets/logo.jpg";
const { isDarkMode, toggleDarkMode } = useLayout();
</script>

<template>
    <div class="topbar">
        <div class="topbar-container">
            <div class="topbar-brand flex items-center">
<!--              <img-->
<!--                  :src="logo"-->
<!--                  alt="Coava Logo"-->
<!--                  class="w-2 h-2 mr-2 object-contain"-->
<!--              />-->
                <span class="topbar-brand-text">
                    <span class="topbar-title">RoasterOps</span>
                    <span class="topbar-subtitle">Coava Inventory Management</span>
                </span>
            </div>
            <div class="topbar-actions">
                <Button type="button" class="topbar-theme-button" @click="toggleDarkMode" text rounded>
                    <i :class="['pi ', 'pi ', { 'pi-moon': isDarkMode, 'pi-sun': !isDarkMode }]" />
                </Button>
                <div class="relative">
                    <Button
                        v-styleclass="{
                            selector: '@next',
                            enterFromClass: 'hidden',
                            enterActiveClass: 'animate-scalein',
                            leaveToClass: 'hidden',
                            leaveActiveClass: 'animate-fadeout',
                            hideOnOutsideClick: true,
                        }"
                        icon="pi pi-cog"
                        text
                        rounded
                        aria-label="Settings"
                    />
                    <AppConfig />
                </div>
            </div>
        </div>
    </div>
</template>
