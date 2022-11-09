import type { Individual, IndividualId } from '~/api/individuals'

export const useFetchAllIndividuals = () => {
  return useQuery(['individuals'], fetchIndividuals, { retry: 0 })
}

export const useAddIndividualMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((individual: Individual) => addIndividual(individual), {
    onMutate: (individual) => {
      const individuals = queryClient.getQueryData<Individual[]>(['individuals'])
      queryClient.setQueryData(['individuals'], individuals ? [...individuals, individual] : undefined)

      return individual
    },
    onSuccess: (data, _, context) => {
      if (!data) {
        return
      }

      const individuals = queryClient.getQueryData<Individual[]>(['individuals'])
      queryClient.setQueryData(['individuals'], individuals?.map(i => i === context ? { ...i, ...data } : i))
    },
    onError: () => queryClient.invalidateQueries(['individuals'])
  })
}

export const useUpdateIndividualMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((individual: Individual) => updateIndividual(individual), {
    onMutate: (individual) => {
      const individuals = queryClient.getQueryData<Individual[]>(['individuals'])
      queryClient.setQueryData(['individuals'], individuals?.map(i => i.id === individual.id ? individual : i))

      return individual
    },
    onSuccess: (data, _, context) => {
      if (!data) {
        return
      }

      const individuals = queryClient.getQueryData<Individual[]>(['individuals'])
      queryClient.setQueryData(['individuals'], individuals?.map(i => i.id === context?.id ? { ...i, ...data } : i))
    },
    onError: () => queryClient.invalidateQueries(['individuals'])
  })
}

export const useRemoveSingleIndividualByIdMutation = () => {
  const queryClient = useQueryClient()
//how it works
  return useMutation((id: IndividualId) => removeIndividual(id), {
    onMutate: (id) => {
      const individuals = queryClient.getQueryData<Individual[]>(['individuals'])
      queryClient.setQueryData(['individuals'], individuals?.filter(i => i.id !== id))
    },
    onError: () => queryClient.invalidateQueries(['individuals'])
  })
}

export const useRemoveMultipleIndividualsByIdMutation = () => {
  const queryClient = useQueryClient()

  return useMutation((ids: IndividualId[]) => removeIndividuals(ids), {
    onMutate: (ids) => {
      const individuals = queryClient.getQueryData<Individual[]>(['individuals'])
      queryClient.setQueryData(['individuals'], individuals?.filter(i => !ids.includes(i.id)))
    },
    onError: () => queryClient.invalidateQueries(['individuals'])
  })
}
