SELECT
	'account' [entityname]
	, a.accountid [entityid]
	, 'address1_stateorprovince' [attributename]
	, 'string' [attributetype]
	, a.ctm_statename [attributevalue]
FROM FilteredAccount a (NOLOCK)
WHERE 
a.statuscode = 1
AND a.owneridname = 'Non Private Client'
AND a.address1_stateorprovince IS NULL AND a.ctm_state IS NOT NULL
UNION
SELECT 
	'account' [entityname]
	, a.accountid [entityid]
	, 'address1_stateorprovince' [attributename]
	, 'string' [attributetype]
	, e.ctm_name [attributevalue]
FROM FilteredAccount a (NOLOCK)
INNER JOIN Filteredctm_state e (NOLOCK) ON a.address1_stateorprovince = e.ctm_uf AND a.address1_country = ctm_countryidname
WHERE 
a.statuscode = 1
AND a.owneridname = 'Non Private Client'