import type { Team, TeamId } from '~/api/teams'

export const useFetchAllTeams = () => {
  return useQuery(['teams'], fetchTeams, { retry: 0 })
}


export const useAddTeamMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((team: Team) => addTeam(team), {
    onMutate: (team) => {
      const teams = queryClient.getQueryData<Team[]>(['teams'])
      queryClient.setQueryData(['teams'], teams ? [...teams, team] : undefined)

      return team
    },

    onSuccess: (data, _, context) => {
      if (!data) {
        return
      }

      const teams = queryClient.getQueryData<Team[]>(['teams'])
      queryClient.setQueryData(['teams'], teams?.map(i => i === context ? { ...i, ...data } : i))
    },
    onError: () => queryClient.invalidateQueries(['teams'])
  })
}

export const useUpdateTeamMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((team: Team) => updateTeam(team), {
    onMutate: (team) => {
      const teams = queryClient.getQueryData<Team[]>(['teams'])
      queryClient.setQueryData(['teams'], teams?.map(i => i.id === team.id ? team : i))

      return team
    },
    onSuccess: (data, _, context) => {
      if (!data) {
        return
      }

      const teams = queryClient.getQueryData<Team[]>(['teams'])
      queryClient.setQueryData(['teams'], teams?.map(i => i.id === context?.id ? { ...i, ...data } : i))
    },
    onError: () => queryClient.invalidateQueries(['teams']) //refetch
  })
}

export const useRemoveSingleTeamByIdMutation = () => {
  const queryClient = useQueryClient()
//how it works
  return useMutation((id: TeamId) => removeTeam(id), {
    onMutate: (id) => {
      const teams = queryClient.getQueryData<Team[]>(['teams'])
      queryClient.setQueryData(['teams'], teams?.filter(i => i.id !== id))
    },
    onError: () => queryClient.invalidateQueries(['teams'])
  })
}

export const useRemoveMultipleTeamsByIdMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((ids: TeamId[]) => removeTeams(ids), {
    onMutate: (ids) => {
      const teams = queryClient.getQueryData<Team[]>(['teams'])
      queryClient.setQueryData(['teams'], teams?.filter(i => !ids.includes(i.id)))
    },
    onError: () => queryClient.invalidateQueries(['teams'])
  })
}

