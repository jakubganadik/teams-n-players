<script setup lang="ts">
  type Props = {
    prependIcon?: string
    label?: string
    appendIcon?: string
  }

  defineProps<Props>()
</script>

<template>
  <button class="d-button">
    <slot>
      <i :class="prependIcon" />
      <span>{{label}}</span>
      <i :class="appendIcon" />
    </slot>
  </button>
</template>

<style>
  :where(.d-button) { @apply bg-transparent text-on-surface }
  .d-button { @apply flex gap-2 items-center h-10 px-4 rounded-5 overflow-clip relative }
  .d-button:disabled { @apply text-on-surface opacity-38 }
  .d-button::after { @apply absolute inset-0 content-empty opacity-8 }
  .d-button:hover:not(:disabled)::after { @apply bg-current }
</style>
