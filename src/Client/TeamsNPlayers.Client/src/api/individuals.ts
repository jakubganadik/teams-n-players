import { output } from 'zod'

const individualsApi = mande('api/v1/individuals')
const api = mande('')

export const individualIdSchema = z.string()

const individualSchema = z.object({
  id: individualIdSchema,
  firstName: z.string(),
  lastName: z.string()
})

export type IndividualId = output<typeof individualIdSchema>
export type Individual = output<typeof individualSchema>

export const fetchIndividuals = async () =>
  individualSchema.array().parse(await individualsApi.get(''))

export const fetchIndividual = async (id: IndividualId) =>
  individualSchema.parse(await individualsApi.get(id))

export const addIndividual = async (individual: Omit<Individual, 'id'>) => {
  const { headers } = await individualsApi.post('', individual, { responseAs: 'response' })
  return individualSchema.parse(await api.get(headers.get('location')!))
}

export const updateIndividual = async (individual: Individual) => {
  await individualsApi.put(individual.id, individual)
  return await fetchIndividual(individual.id)
}

export const removeIndividual = async (id: IndividualId) =>
  await individualsApi.delete(id)

export const removeIndividuals = async (ids: IndividualId[]) =>
  await individualsApi.post('delete', ids)
