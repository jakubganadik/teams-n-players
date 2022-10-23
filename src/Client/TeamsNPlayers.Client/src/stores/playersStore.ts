import { z } from 'zod'

const playerSchema = z.object({
  firstName: z.string(),
  lastName: z.string(),
})

type P = z.infer<typeof playerSchema>

export const usePlayersStore = defineStore('players', () => {
  const teamId = ref<string>()
  const playersUrl = computed(() => `api/v1/teams/${teamId.value}/players`)

  const { isFetching, error, data, execute } = useFetch<P[]>(playersUrl, {
    initialData: [],
    afterFetch: (ctx) => ({... ctx, data: playerSchema.array().parse(ctx.data) }),
    immediate: false
  }).get().json()

  const loadPlayers = (team: string) => {
    teamId.value = team
    return execute()
  }

  return {
    players: computed(() => !isFetching.value && !error.value ? data : []),
    loadPlayers
  }
})
