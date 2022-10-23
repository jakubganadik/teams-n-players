export const useFetchAllTeams = () => {
  return useQuery(['teams'], fetchTeams, { retry: 0 })
}
