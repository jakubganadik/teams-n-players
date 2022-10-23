<script setup lang="ts">
  type Props = {
    label: string
    modelValue: string | null
    options: { key: string, label: string }[]
  }

  const props = defineProps<Props>()
  const emit = defineEmits(['update:modelValue'])

  const model = useVModel(props, 'modelValue', emit)

</script>

<template>
  <label>
    {{ label }}
    <select v-model="model">
      <option v-for="option in options" :value="option.key">{{ option.label }}</option>
    </select>
  </label>
</template>

<style scoped>
  select { @apply border-outline bg-transparent focus:ring-primary focus:border-primary }
  select > option { @apply bg-surface }
  label { @apply label-large flex flex-col gap-1 }
</style>
