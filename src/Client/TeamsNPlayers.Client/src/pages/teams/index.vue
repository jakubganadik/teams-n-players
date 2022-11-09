<script setup lang="ts">
  import type { Team } from '~/api/teams'

  const { data: teamsData } = useFetchAllTeams()

  const teams = refDefault(teamsData, []) //[] if no fetch data

  const { selectedKeys, selectedCount, isSelected, isAllSelected, toggle, toggleAll } = useSelectable(teams, team => team.id)//function as an argument

  const { mutate: add } = useAddTeamMutation()
  const { mutate: update } = useUpdateTeamMutation()
  const { mutate: remove } = useRemoveSingleTeamByIdMutation()
  const { mutate: removeMultiple } = useRemoveMultipleTeamsByIdMutation()

  const teamToCreate = ref<Team>()
  const teamToEdit = ref<Team>()
  const teamToRemove = ref<Team>()

  const showBulkRemove = ref(false)

</script>

<template>
  <div class="min-h-full flex flex-col">
    <header class="flex justify-between">
      <h1 class="text-12 py-4">Teams</h1>
    </header>

    <div class="h-16 flex items-center px-3 gap-3">
      <div class="flex-1 text-primary">
        <template v-if="selectedCount">
          {{ selectedCount }} of {{ teams.length }} selected
        </template>
      </div>

      <d-filled-button
        v-if="selectedCount"
        @click="showBulkRemove = true"
        prepend-icon="icon-delete"
        label="Delete selected teams"
        class="bg-error-container text-on-error-container"
      />

      <d-filled-button
        v-else
        @click="teamToCreate = { name: ''}"
        prepend-icon="icon-add"
        label="Create new team"
        class="bg-primary text-on-primary"
      />
    </div>

    <table v-if="!!teams.length" class="w-full">
      <thead>
      <tr class="h-14">
        <th class="w-12 pl-3">
          <d-checkbox class="text-primary" :checked="isAllSelected" @change="toggleAll()" />
        </th>
        <th>name</th>
        
      </tr>
      </thead>
      <tbody>
      <tr v-for="team in teams" class="h-13 border-t-1 border-outline group">
        <td class="pl-3">
          <d-checkbox class="text-primary" :checked="isSelected(team)" @change="toggle(team)" />
        </td>
        <td>{{ team.name }}</td>
        <td>
          <div class="invisible flex flex-row justify-end group-hover:visible">
            <d-icon-button @click="teamToEdit = { ...team }" icon="icon-edit" class="hover:text-primary" />
            <d-icon-button @click="teamToRemove = team" icon="icon-delete" class="hover:text-error" />
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
    title="Create Team"
    v-model="teamToCreate"
    confirm-label="Create"
    @confirm="add"
    :confirm-disabled="!teamToCreate?.name"
  >
    <d-text-input label="name" v-model="teamToCreate.name" />
  </d-model-dialog>

  <d-model-dialog
    title="Update team"
    v-model="teamToEdit"
    confirm-label="Update"
    @confirm="update"
    :confirm-disabled="!teamToEdit?.name"
  >
    <d-text-input label="name" v-model="teamToEdit.name" />
  </d-model-dialog>

  <d-model-dialog
    title="Delete team"
    v-model="teamToRemove"
    confirm-label="Delete"
    @confirm="remove(teamToRemove?.id)"
  >
    <div>
      <p> Are you sure you want to permanently delete "{{ teamToRemove.name }}"? </p>
      <p> This action is permanent and can't be reversed. </p>
    </div>
  </d-model-dialog>

  <d-dialog
    title="Delete selected teams"
    v-model="showBulkRemove"
    confirm-label="Delete selected"
    @confirm="removeMultiple(selectedKeys); showBulkRemove = false"
    @cancel="showBulkRemove = false"
  >
    <div>
      <p> Are you sure you want to permanently delete {{ selectedCount }} teams? </p>
      <p> This action is permanent and can't be reversed. </p>
    </div>
  </d-dialog>
</template>

<style>
  th { @apply text-left }
</style>
