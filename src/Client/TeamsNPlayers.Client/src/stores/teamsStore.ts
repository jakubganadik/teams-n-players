import { z } from 'zod'

const teamsSchema = z.object({
  id: z.string(),
  name: z.string()
})

type Team = z.infer<typeof teamsSchema>

export const useTeamsStore = defineStore('teams', () => {
  const { isFetching, error, data } = useFetch<Team[]>(`api/v1/teams`, {
    initialData: [],
    afterFetch: (ctx) => ({... ctx, data: teamsSchema.array().parse(ctx.data) }),
  }).get().json()

  watch(data, () => console.log('data', data.value))
  watch(isFetching, () => console.log('isFetching', isFetching.value))
  watch(error, () => console.log('error', error.value))

  return {
    isFetching: readonly(isFetching),
    error: computed(() => !isFetching.value ? error : undefined),
    teams: computed(() => !isFetching.value && !error.value ? data.value as Team[] : []),
  }
})
