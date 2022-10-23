<script setup lang="ts" >
  type Props = {
    modelValue: any | undefined,
    title?: string,
    confirmLabel?: string,
    confirmDisabled?: boolean
  }

  const props = defineProps<Props>()
  const emit = defineEmits<{
    (e: 'update:modelValue'): void,
    (e: 'cancel'): void,
    (e: 'confirm', value: typeof props.modelValue): void
  }>()

  const model = useVModel(props, 'modelValue', emit)
</script>

<template>
  <d-dialog
    :title="title"
    :model-value="!!model"
    @cancel="emit('cancel'); model = undefined"
    @confirm="emit('confirm', model); model = undefined"
    :confirm-label="confirmLabel"
    :confirm-disabled="confirmDisabled"
  >
    <slot v-if="!!model" />
  </d-dialog>
</template>

<style>
  :where(.d-dialog) { @apply bg-surface text-on-surface elevation-2 rounded-6 pa-6 flex flex-col min-w-128 gap-4 }
</style>
