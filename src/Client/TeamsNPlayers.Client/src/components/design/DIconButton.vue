<script setup lang="ts">
  type Props = {
    icon?: string
  }

  defineProps<Props>()
</script>

<template>
  <button class="d-icon-button">
    <i :class="icon" />
  </button>
</template>

<style>
  :where(.d-icon-button) { @apply w-10 h-10 grid place-items-center transition duration-200 rounded-5 overflow-clip }
  :where(.d-icon-button):hover { @apply elevation-2 }
</style>
