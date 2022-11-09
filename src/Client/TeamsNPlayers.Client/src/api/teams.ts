import { output } from 'zod'

const teamsApi = mande('api/v1/teams')
const api = mande('')

export const teamIdSchema = z.string()

const teamSchema = z.object({
  id: teamIdSchema,
  name: z.string(),
})


export type TeamId = output<typeof teamIdSchema>
export type Team = output<typeof teamSchema>


export const fetchTeams = async () =>
  teamSchema.array().parse(await teamsApi.get(''))

export const fetchTeam = async (id: TeamId) =>
  teamSchema.parse(await teamsApi.get(id))

export const addTeam = async (team: Omit<Team, 'id'>) => {
  const { headers } = await teamsApi.post('', team, { responseAs: 'response' })
  return teamSchema.parse(await api.get(headers.get('location')!))
}

export const updateTeam = async (team: Team) => {
  await teamsApi.put(team.id, team)
  return await fetchTeam(team.id)
}

export const removeTeam = async (id: TeamId) =>
  await teamsApi.delete(id)

export const removeTeams = async (ids: TeamId[]) =>
  await teamsApi.post('delete', ids)
