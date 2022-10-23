<script setup lang="ts">
  import type { Player } from '~/api/players'
  import { availableRoles } from '~/api/roles'

  const { data: playersData } = useFetchAllPlayers()
  const players = refDefault(playersData, [])

  const { data: individualsData } = useFetchAllIndividuals()
  const individuals = computed(() => individualsData.value?.map(i => ({ key: i.id, label: `${i.lastName.toUpperCase()} ${i.firstName}`})) ?? [] )

  const { data: teamsData } = useFetchAllTeams()
  const teams = computed(() => teamsData.value?.map(t => ({ key: t.id, label: t.name})) ?? [] )

  const { selectedKeys, selectedCount, isSelected, isAllSelected, toggle, toggleAll } = useSelectable(players, player => player.id)

  const { mutate: add } = useAddPlayerMutation()
  const { mutate: update } = useUpdatePlayerMutation()
  const { mutate: remove } = useRemoveSinglePlayerByIdMutation()
  const { mutate: removeMultiple } = useRemoveMultiplePlayersByIdMutation()

  const playerToCreate = ref<Player>()
  const playerToEdit = ref<Player>()
  const playerToRemove = ref<Player>()

  const showBulkRemove = ref(false)

  const playerValidationFailed = (player: Player) => {
    return !player
      || !player.individualId
      || !player.teamId
      || !player.position
      || !parseInt(player.shirtNumber + '')
      || player.shirtNumber < 0
  }

  const emptyPlayer = () => ({
    teamId: null,
    individualId: null,
    shirtNumber: null,
    position: null
  })

  const availableRolesOptions = [...availableRoles].map(([key, label]) => ({ key, label }))

</script>

<template>
  <div class="min-h-full flex flex-col">
    <header class="flex justify-between">
      <h1 class="text-12 py-4">Players</h1>
    </header>

    <div class="h-16 flex items-center px-3 gap-3">
      <div class="flex-1 text-primary">
        <template v-if="selectedCount">
          {{ selectedCount }} of {{ players.length }} selected
        </template>
      </div>

      <d-filled-button
        v-if="selectedCount"
        @click="showBulkRemove = true"
        prepend-icon="icon-delete"
        label="Delete selected players"
        class="bg-error-container text-on-error-container"
      />

      <d-filled-button
        v-else
        @click="playerToCreate = emptyPlayer()"
        prepend-icon="icon-add"
        label="Create new player"
        class="bg-primary text-on-primary"
      />
    </div>

    <table v-if="!!players.length" class="w-full">
      <thead>
      <tr class="h-14">
        <th class="w-12 pl-3">
          <d-checkbox class="text-primary" :checked="isAllSelected" @change="toggleAll()" />
        </th>
        <td>Team</td>
        <td>#</td>
        <td>Player name</td>
        <td>Position</td>
        <th class="w-48" />
      </tr>
      </thead>
      <tbody>
      <tr v-for="player in players" class="h-13 border-t-1 border-outline group">
        <td class="pl-3">
          <d-checkbox class="text-primary" :checked="isSelected(player)" @change="toggle(player)" />
        </td>
        <td>{{ player.teamName }}</td>
        <td>{{ player.shirtNumber }}</td>
        <td>{{ player.playerName }}</td>
        <td>{{ availableRoles.get(player.position) }}</td>
        <td>
          <div class="invisible flex flex-row justify-end group-hover:visible">
            <d-icon-button @click="playerToEdit = { ...player }" icon="icon-edit" class="hover:text-primary" />
            <d-icon-button @click="playerToRemove = player" icon="icon-delete" class="hover:text-error" />
          </div>
        </td>
      </tr>
      </tbody>
    </table>

    <div v-else class="flex-1 grid place-items-center display-small text-surface-variant">
      No players found
    </div>
  </div>

  <d-model-dialog
    title="Create player"
    v-model="playerToCreate"
    confirm-label="Create"
    @confirm="add"
    :confirm-disabled="playerValidationFailed(playerToCreate)"
  >
    <d-select label="Team" v-model="playerToCreate.teamId" :options="teams" />
    <d-select label="Individual" v-model="playerToCreate.individualId" :options="individuals" />
    <d-text-input type="number" label="Shirt number" v-model="playerToCreate.shirtNumber" />
    <d-select label="Position" v-model="playerToCreate.position" :options="availableRolesOptions" />
  </d-model-dialog>

  <d-model-dialog
    title="Update player"
    v-model="playerToEdit"
    confirm-label="Update"
    @confirm="update"
    :confirm-disabled="playerValidationFailed(playerToEdit)"
  >
    <d-select label="Team" v-model="playerToEdit.teamId" :options="teams" />
    <d-select label="Individual" v-model="playerToEdit.individualId" :options="individuals" />
    <d-text-input type="number" label="Shirt number" v-model="playerToEdit.shirtNumber" />
    <d-select label="Position" v-model="playerToEdit.position" :options="availableRolesOptions" />
  </d-model-dialog>

  <d-model-dialog
    title="Delete player"
    v-model="playerToRemove"
    confirm-label="Delete"
    @confirm="remove(playerToRemove?.id)"
  >
    <div>
      <p> Are you sure you want to remove player "{{ playerToRemove.playerName }}" from "{{ playerToRemove.teamName }}"? </p>
      <p> This action is permanent and can't be reversed. </p>
    </div>
  </d-model-dialog>

  <d-dialog
    title="Delete selected players"
    v-model="showBulkRemove"
    confirm-label="Delete selected"
    @confirm="removeMultiple(selectedKeys); showBulkRemove = false"
    @cancel="showBulkRemove = false"
  >
    <div>
      <p> Are you sure you want to permanently remove {{ selectedCount }} players from their respective teams? </p>
      <p> This action is permanent and can't be reversed. </p>
    </div>
  </d-dialog>
</template>

<style>
  th { @apply text-left }
</style>
