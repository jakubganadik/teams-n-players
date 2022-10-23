<script setup lang="ts">

</script>

<template>
  <input
    type="checkbox"
  />
</template>

<style scoped>
  input { @apply !ring-0 !w-4 !h-4 bg-transparent checked:icon-check?bg checked:bg-current }
</style>
