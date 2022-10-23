export const teamIdSchema = z.string()

const api = mande('api/v1/teams')

export const fetchTeams = async () => await api.get<{ id: string, name: string }[]>('')
