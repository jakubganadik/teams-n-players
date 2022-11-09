import type { output } from 'zod'

const api = mande('')
const playersApi = mande('api/v1/players')

const playerIdSchema = z.string()

const playerSchema = z.object({
  id: playerIdSchema,
  teamId: teamIdSchema,
  individualId: individualIdSchema,
  teamName: z.string(),
  playerName: z.string(),
  shirtNumber: z.number(),
  position: z.string()
})

export type PlayerId = output<typeof playerIdSchema>
export type Player = output<typeof playerSchema>

export const fetchPlayers = async () =>
  playerSchema.array().parse(await playersApi.get(''))
//calling backend
export const fetchPlayer = async (id: PlayerId) =>
  playerSchema.parse(await playersApi.get(id))

export const addPlayer = async (player: Omit<Player, 'id'>) => {
  const { headers } = await playersApi.post('', player, { responseAs: 'response' })
  return playerSchema.parse(await api.get(headers.get('location')!))
}

export const updatePlayer = async (player: Player) => {
  await playersApi.put(player.id, player)
  return await fetchPlayer(player.id)
}

export const removePlayer = async (id: PlayerId) =>
  await playersApi.delete(id)

export const removePlayers = async (ids: PlayerId[]) =>
  await playersApi.post('delete', ids)
