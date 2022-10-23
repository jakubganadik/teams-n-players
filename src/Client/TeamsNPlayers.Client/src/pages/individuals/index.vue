<script setup lang="ts">
  import type { Individual } from '~/api/individuals'

  const { data: individualsData } = useFetchAllIndividuals()

  const individuals = refDefault(individualsData, [])

  const { selectedKeys, selectedCount, isSelected, isAllSelected, toggle, toggleAll } = useSelectable(individuals, individual => individual.id)

  const { mutate: add } = useAddIndividualMutation()
  const { mutate: update } = useUpdateIndividualMutation()
  const { mutate: remove } = useRemoveSingleIndividualByIdMutation()
  const { mutate: removeMultiple } = useRemoveMultipleIndividualsByIdMutation()

  const individualToCreate = ref<Individual>()
  const individualToEdit = ref<Individual>()
  const individualToRemove = ref<Individual>()

  const showBulkRemove = ref(false)

</script>

<template>
  <div class="min-h-full flex flex-col">
    <header class="flex justify-between">
      <h1 class="text-12 py-4">Individuals</h1>
    </header>

    <div class="h-16 flex items-center px-3 gap-3">
      <div class="flex-1 text-primary">
        <template v-if="selectedCount">
          {{ selectedCount }} of {{ individuals.length }} selected
        </template>
      </div>

      <d-filled-button
        v-if="selectedCount"
        @click="showBulkRemove = true"
        prepend-icon="icon-delete"
        label="Delete selected individuals"
        class="bg-error-container text-on-error-container"
      />

      <d-filled-button
        v-else
        @click="individualToCreate = { firstName: '', lastName: '' }"
        prepend-icon="icon-add"
        label="Create new individual"
        class="bg-primary text-on-primary"
      />
    </div>

    <table v-if="!!individuals.length" class="w-full">
      <thead>
      <tr class="h-14">
        <th class="w-12 pl-3">
          <d-checkbox class="text-primary" :checked="isAllSelected" @change="toggleAll()" />
        </th>
        <th>Last name</th>
        <th class="w-48">First name</th>
        <th class="w-48" />
      </tr>
      </thead>
      <tbody>
      <tr v-for="individual in individuals" class="h-13 border-t-1 border-outline group">
        <td class="pl-3">
          <d-checkbox class="text-primary" :checked="isSelected(individual)" @change="toggle(individual)" />
        </td>
        <td>{{ individual.lastName }}</td>
        <td>{{ individual.firstName }}</td>
        <td>
          <div class="invisible flex flex-row justify-end group-hover:visible">
            <d-icon-button @click="individualToEdit = { ...individual }" icon="icon-edit" class="hover:text-primary" />
            <d-icon-button @click="individualToRemove = individual" icon="icon-delete" class="hover:text-error" />
          </div>
        </td>
      </tr>
      </tbody>
    </table>

    <div v-else class="flex-1 grid place-items-center display-small text-surface-variant">
      No individuals found
    </div>
  </div>

  <d-model-dialog
    title="Create individual"
    v-model="individualToCreate"
    confirm-label="Create"
    @confirm="add"
    :confirm-disabled="!individualToCreate?.lastName || !individualToCreate?.firstName"
  >
    <d-text-input label="Last name" v-model="individualToCreate.lastName" />
    <d-text-input label="First name" v-model="individualToCreate.firstName" />
  </d-model-dialog>

  <d-model-dialog
    title="Update individual"
    v-model="individualToEdit"
    confirm-label="Update"
    @confirm="update"
    :confirm-disabled="!individualToEdit?.lastName || !individualToEdit?.firstName"
  >
    <d-text-input label="Last name" v-model="individualToEdit.lastName" />
    <d-text-input label="First name" v-model="individualToEdit.firstName" />
  </d-model-dialog>

  <d-model-dialog
    title="Delete individual"
    v-model="individualToRemove"
    confirm-label="Delete"
    @confirm="remove(individualToRemove?.id)"
  >
    <div>
      <p> Are you sure you want to permanently delete "{{ individualToRemove.lastName }}, {{ individualToRemove.firstName }}"? </p>
      <p> This action is permanent and can't be reversed. </p>
    </div>
  </d-model-dialog>

  <d-dialog
    title="Delete selected individuals"
    v-model="showBulkRemove"
    confirm-label="Delete selected"
    @confirm="removeMultiple(selectedKeys); showBulkRemove = false"
    @cancel="showBulkRemove = false"
  >
    <div>
      <p> Are you sure you want to permanently delete {{ selectedCount }} individuals? </p>
      <p> This action is permanent and can't be reversed. </p>
    </div>
  </d-dialog>
</template>

<style>
  th { @apply text-left }
</style>
