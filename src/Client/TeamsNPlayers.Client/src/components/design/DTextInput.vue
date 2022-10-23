<script lang="ts">
export default { inheritAttrs: false }
</script>

<script setup lang="ts">
  type Props = {
    label: string
    modelValue: number | string | null
    type?: 'text' | 'number'
  }

  const props = defineProps<Props>()
  const emit = defineEmits(['update:modelValue'])

  const model = useVModel(props, 'modelValue', emit)

  const attrs = useAttrs()

</script>

<template>
  <label>
    {{ label }}
    <input v-bind="attrs" v-model="model" :type="type ?? 'text'" />
  </label>
</template>

<style scoped>
  input[type='text'], input[type='number'] { @apply border-outline bg-transparent focus:ring-primary focus:border-primary }
  label { @apply label-large flex flex-col gap-1 }
</style>
