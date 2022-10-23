<script setup lang="ts">
  type Props = {
    modelValue?: boolean,
    title?: string,
    confirmLabel?: string,
    confirmDisabled?: boolean
  }

  const props = defineProps<Props>()

  const emit = defineEmits(['update:modelValue', 'cancel', 'confirm', 'close'])
  const model = useVModel(props, 'modelValue', emit)
</script>

<template>
  <modal-dialog v-model="model" class="d-dialog max-h-screen min-h-42" @close="emit('close')">
    <h3 v-if="title" class="headline-small">{{ title }}</h3>

    <div class="flex-1 flex flex-col gap-4 overflow-y-auto overflow-y-scroll scrollbar scrollbar-rounded scrollbar-w-1 scrollbar-track-color-transparent scrollbar-thumb-color-surface-variant">
      <slot />
    </div>

    <div class="flex justify-end gap-2 mt-2">
      <d-filled-button @click="emit('cancel')" label="Cancel" class="text-primary" />
      <d-filled-button :disabled="confirmDisabled" @click="emit('confirm')" :label="confirmLabel ?? 'Confirm'" class="text-primary" />
    </div>
  </modal-dialog>
</template>

<style>
  :where(.d-dialog) { @apply bg-surface text-on-surface elevation-2 rounded-6 pa-6 flex flex-col min-w-128 gap-4 }
</style>
