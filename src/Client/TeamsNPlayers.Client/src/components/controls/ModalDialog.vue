<script lang="ts">
  export default { inheritAttrs: false }
</script>

<script setup lang="ts">

type Props = {
  modelValue?: boolean
}

const props = defineProps<Props>()
const emit = defineEmits(['update:modelValue', 'close'])

const model = useVModel(props, 'modelValue', emit)

const attrs = useAttrs()

const dialog = ref<HTMLDialogElement>()

watch([computed(() => props.modelValue), dialog], ([open]) => {
  if (open) {
    dialog.value?.showModal()
  } else {
    dialog.value?.close()
    emit('close')
  }
})

</script>

<template>
  <teleport to="body">
    <dialog class="p-0 bg-transparent max-h-screen min-h-64" ref="dialog">
      <div v-bind="attrs" class="overflow-auto">
        <slot />
      </div>
    </dialog>
  </teleport>
</template>
