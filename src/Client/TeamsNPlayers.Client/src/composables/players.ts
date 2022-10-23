import type { Player, PlayerId } from '~/api/players'

export const useFetchAllPlayers = () => {
  return useQuery(['players'], fetchPlayers, { retry: 0 })
}

export const useAddPlayerMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((player: Player) => addPlayer(player), {
    onMutate: (player) => {
      const players = queryClient.getQueryData<Player[]>(['players'])
      queryClient.setQueryData(['players'], players ? [...players, player] : undefined)

      return player
    },
    onSuccess: (data, _, context) => {
      if (!data) {
        return
      }

      const players = queryClient.getQueryData<Player[]>(['players'])
      queryClient.setQueryData(['players'], players?.map(i => i === context ? { ...i, ...data } : i))
    },
    // onError: () => queryClient.invalidateQueries(['players'])
  })
}

export const useUpdatePlayerMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((player: Player) => updatePlayer(player), {
    onMutate: (player) => {
      const players = queryClient.getQueryData<Player[]>(['players'])
      queryClient.setQueryData(['players'], players?.map(i => i.id === player.id ? player : i))

      return player
    },
    onSuccess: (data, _, context) => {
      if (!data) {
        return
      }

      const players = queryClient.getQueryData<Player[]>(['players'])
      queryClient.setQueryData(['players'], players?.map(i => i.id === context?.id ? { ...i, ...data } : i))
    },
    // onError: () => queryClient.invalidateQueries(['players'])
  })
}

export const useRemoveSinglePlayerByIdMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((id: PlayerId) => removePlayer(id), {
    onMutate: (id) => {
      const players = queryClient.getQueryData<Player[]>(['players'])
      queryClient.setQueryData(['players'], players?.filter(i => i.id !== id))
    },
    // onError: () => queryClient.invalidateQueries(['players'])
  })
}

export const useRemoveMultiplePlayersByIdMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((ids: PlayerId[]) => removePlayers(ids), {
    onMutate: (ids) => {
      const players = queryClient.getQueryData<Player[]>(['players'])
      queryClient.setQueryData(['players'], players?.filter(i => !ids.includes(i.id)))
    },
    // onError: () => queryClient.invalidateQueries(['players'])
  })
}
